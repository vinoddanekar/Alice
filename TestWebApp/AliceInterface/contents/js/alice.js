var responseBox;
var chatBox;
var recentMessageQueue = new MessageQueue();

$(document).ready(function () {
    adjustLayout();
    responseBox = $('#responseBox');
    chatBox = $('#chatBox');
    chatBox.focus();
    var keyboardHandler = new KeyBoardHandler();
    keyboardHandler.chatBox = chatBox;
    keyboardHandler.postRequestCallBack = postRequest;
    keyboardHandler.recentMessageQueue = recentMessageQueue;
    keyboardHandler.handle();
    requestMessageApi("WelcomeMessage");
    $(window).resize(function () {
        adjustLayout();
    });
});

function scrollResponseToBottom() {
    responseBox.animate({
        scrollTop: responseBox.get(0).scrollHeight
    }, 300);
}

function postRequest(message) {
    recentMessageQueue.Add(message);

    var trimmedMessage = message.trim();
    if (trimmedMessage != "") {
        showUserMessage(message);
        requestMessageApi(message);
    }
}

function showUserMessage(message) {
    var userMessageTemplate = $('#userMessage').html();
    var messageToShow = userMessageTemplate.replace('{Message}', message);
    responseBox.append(messageToShow);
}

function showAliceMessage(message) {
    var aliceMessageTemplate = $('#aliceMessage').html();
    var messageToShow = aliceMessageTemplate.replace('{Message}', message);
    responseBox.append(messageToShow);
}

function showAliceErrorMessage(message) {
    var aliceMessageTemplate = $('#aliceError');
    var aliceMessageTemplateHtml = aliceMessageTemplate.html();

    var messageToShow = aliceMessageTemplateHtml.replace('{Message}', message);
    responseBox.append(messageToShow);
}

function adjustLayout() {
    var chatBox = $('#chatBox');
    var windowWidth = $(window).width();
    var expectedWidth = windowWidth - 26;
    chatBox.width(expectedWidth + 'px');
}

function prepareRequest(message) {
    var request = {};
    request.Message = message;
    var requestEncloser = {};
    requestEncloser.request = request;
    var requestString = JSON.stringify(requestEncloser);
    
    return requestString;
}

function requestMessageApi(message) {
    var url = "alice-chat.aspx/Ask";
    var requestString = prepareRequest(message);
    $.ajax({
        type: "POST",
        url: url,
        data: requestString,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: processSuccessResponse,
        failure: processErrorResponse,
        error: processErrorResponse
    });
}

function processSuccessResponse(response) {
    var actionToPerform = response.d.ActionToPerform;
    if (response.d.StatusCode != 200) {
        processErrorResponse(response.d);
        return;
    }

    if (actionToPerform != "") {
        performAction(actionToPerform);
    }

    var message = response.d.Message;
    message = parseAliceRequestIfAny(message);
    showAliceMessage(message);
    scrollResponseToBottom();
}

function parseAliceRequestIfAny(responseText) {
    var result = responseText.replace(/{aliceRequestAct}/g, "class=\"aliceRequestMarker\" href=\"#\" onclick=\"javascript:autoPlaceRequest(this);\"");
    result = responseText.replace(/{aliceRequestHint}/g, "class=\"aliceRequestMarker\" href=\"#\" onclick=\"javascript:autoHintRequest(this);\"");
    return result;
}

function readRequestFromAnchor(sender) {
    var senderObject = $(sender);
    var requestText;
    requestText = senderObject.data('request');

    if (!requestText)
        requestText = senderObject.text();

    return requestText;
}

function autoPlaceRequest(sender) {
    var requestText = readRequestFromAnchor(sender);
    postRequest(requestText);
    return true;
}

function autoHintRequest(sender) {
    var requestText = readRequestFromAnchor(sender);
    chatBox.val(requestText);
    return true;
}

function processErrorResponse(response) {
    var errorMessage;

    if (response.StatusCode === 400) {
        errorMessage = response.Message;
    } else if (response.status === 0) {
        errorMessage = "Oh boy! Seems you are not connected to server or server is down.";
    } else if (response.status === 500) {
        errorMessage = response.responseJSON.Message;
    }

    errorMessage = parseAliceRequestIfAny(errorMessage);
    showAliceErrorMessage(errorMessage);
    scrollResponseToBottom();
}

function performAction(actionToPerform) {
    if (actionToPerform === "clear") {
        responseBox.html("");
    }
}
