var responseBox;
var chatBox;
var recentMessages = [];
var currentRecentMessageIndex = 0;
function addToRecent(message) {
    if (recentMessages.length === 10) {
        recentMessages.shift();
    }
    recentMessages.push(message);
    currentRecentMessageIndex = 9;
}

function getPrevMessage() {
    currentRecentMessageIndex = currentRecentMessageIndex - 1;
    if (currentRecentMessageIndex < 0 || currentRecentMessageIndex >= recentMessages.length) {
        currentRecentMessageIndex = recentMessages.length - 1;
    }

    return recentMessages[currentRecentMessageIndex];
}

function getNextMessage() {
    currentRecentMessageIndex = currentRecentMessageIndex + 1;
    if (currentRecentMessageIndex >= recentMessages.length || currentRecentMessageIndex < 0) {
        currentRecentMessageIndex = 0;
    }
    return recentMessages[currentRecentMessageIndex];
}

$(document).ready(function () {
    adjustLayout();
    responseBox = $('#responseBox');
    chatBox = $('#chatBox');
    chatBox.focus();

    $('#chatBox').keydown(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode === 13) {
            var message = $(this).val();
            processMessage(message);
            $(this).val("");
        }
        else if (keycode === 38) {
            var message = getPrevMessage();
            $(this).val(message);
        }
        else if (keycode === 40) {
            var message = getNextMessage();
            $(this).val(message);
        }
        else if (keycode === 27) {
            $(this).val("");
        }
    });

    $(window).resize(function () {
        adjustLayout();
    });
});

function scrollResponseToBottom() {
    responseBox.animate({
        scrollTop: responseBox.get(0).scrollHeight
    }, 300);
}

function processMessage(message) {
    addToRecent(message);

    var trimmedMessage = message.trim();
    if (trimmedMessage != "") {
        showUserMessage(message);
        postMessage(message);
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
    var aliceMessageTemplate = $('#aliceMessage');
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

function postMessage(message) {
    requestMessageApi(message);
}

function requestMessageApi(message) {
    var url = "alice-chat.aspx/Ask";
    message = message.replace("\'", "");

    var jsonRequest = "{message:\'" + message + "\'}";
    $.ajax({
        type: "POST",
        url: url,
        data: jsonRequest,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: processReply,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            processErrorReply(response);
        }
    });
}

function processReply(response) {
    var message = response.d.Message;
    var actionToPerform = response.d.ActionToPerform;
    if (response.d.StatusCode != 200) {
        processErrorReply(response.d);
        return;
    }

    if (actionToPerform != "") {
        performAction(actionToPerform);
    }

    message = parseAliceRequestIfAny(message);
    showAliceMessage(message);
    scrollResponseToBottom();
}

function parseAliceRequestIfAny(responseText) {
    var result = responseText.replace(/{aliceRequestAct}/g, "class=\"aliceRequestMarker\" href=\"#\" onclick=\"javascript:autoPlaceRequest(this);\"");
    result = responseText.replace(/{aliceRequestHint}/g, "class=\"aliceRequestMarker\" href=\"#\" onclick=\"javascript:autoHintRequest(this);\"");
    return result;
}

function autoPlaceRequest(sender) {
    var senderObject = $(sender);
    var requestText = senderObject.text();
    processMessage(requestText);
    return true;
}

function autoHintRequest(sender) {
    var senderObject = $(sender);
    var requestText = senderObject.text();
    chatBox.val(requestText);
    return true;
}

function processErrorReply(response) {
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

