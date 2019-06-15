﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alice-chat.aspx.cs" Inherits="TestWebApp.AliceInterface.Alice_Chat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="contents/js/jquery-3.4.1.min.js"></script>
    <link href="contents/css/default.css" rel="stylesheet" />
    <link href="contents/css/animations.css" rel="stylesheet" />
    <script src="contents/js/alice.js"></script>

    <script type="text/javascript">
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
                <div class="userResponse slide-in-fwd-bottom ">
                    <span>You:</span> {Message}
                </div>
            </div>
        </div>
        <div id="aliceMessage">
            <div class="response">
                <div class="aliceResponse slide-in-fwd-top ">
                    <span>Alice:</span> {Message}
                </div>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    </form>
</body>
</html>
