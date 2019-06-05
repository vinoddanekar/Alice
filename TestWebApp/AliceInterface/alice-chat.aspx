<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alice-chat.aspx.cs" Inherits="TestWebApp.AliceInterface.Alice_Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="contents/js/jquery-3.4.1.min.js"></script>
    <link href="contents/css/default.css" rel="stylesheet" />

    <script type="text/javascript">
        var responseBox;
        var recentMessages = [];
        var currentRecentMessageIndex = 0;
        function addToRecent(message) {
            if (recentMessages.length == 10) {
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

            $('#chatBox').keydown(function (event) {
                var keycode = (event.keyCode ? event.keyCode : event.which);
                if (keycode == '13') {
                    var message = $(this).val();
                    addToRecent(message);
                    processMessage(message);
                    $(this).val("");
                }
                else if (keycode == "38") {
                    var message = getPrevMessage();
                    $(this).val(message);
                }
                else if (keycode == "40") {
                    var message = getNextMessage();
                    $(this).val(message);
                }
                else if (keycode == "27") {
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
            }, 50);
        }

        function processMessage(message) {
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
                }
            });
        }

        function processReply(response) {
            var message = response.d.Message;
            var actionToPerform = response.d.ActionToPerform;

            if (actionToPerform != "") {
                performAction(actionToPerform);
            }

            showAliceMessage(message);
            scrollResponseToBottom();
        }

        function performAction(actionToPerform) {
            if (actionToPerform == "clear") {
                responseBox.html("");
            }
        }


    </script>
</head>
<body>

    <div id="content">
        <div id="header">
            <h3>Alice</h3>
        </div>
        <div id="responseBox">
        </div>
        <input id="chatBox" placeholder="Ask something..." type="text" autocomplete="off" />
    </div>
    <div style="display: none">
        <div id="userMessage">
            <div class="response">
                <div class="userResponse">
                    <span>You:</span> {Message}
                </div>
            </div>
        </div>
        <div id="aliceMessage">
            <div class="response">

                <div class="aliceResponse">
                    <span>Alice:</span> {Message}
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    </form>
</body>
</html>
