var KeyBoardHandler = function () {
    this.chatBox = null;
    this.postRequestCallBack = null;
    this.recentMessageQueue = null;
}

KeyBoardHandler.prototype.handle = function () {
    this.attachChatBoxKeyboardEvent();
}

KeyBoardHandler.prototype.attachChatBoxKeyboardEvent = function() {
    var sender = this;
    this.chatBox.keydown(function (event) {
        sender.handleChatBoxKeyboardEvent(sender, event);
    });
}

KeyBoardHandler.prototype.handleChatBoxKeyboardEvent = function (sender, event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode === 13) {
        var message = sender.chatBox.val();
        sender.postRequestCallBack(message);
        sender.chatBox.val("");
    }
    else if (keycode === 38) {
        var message = sender.recentMessageQueue.Previous();
        sender.chatBox.val(message);
    }
    else if (keycode === 40) {
        var message = sender.recentMessageQueue.Next();
        sender.chatBox.val(message);
    }
    else if (keycode === 27) {
        sender.chatBox.val("");
    }
}

