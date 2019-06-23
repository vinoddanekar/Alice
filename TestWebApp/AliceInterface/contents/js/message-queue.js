var MessageQueue = function () {
    this.currentIndex = 0;
    this.messages = [];
    this.capacity = 10;
}

MessageQueue.prototype.Previous = function () {
    this.currentIndex = this.currentIndex - 1;
    if (this.currentIndex < 0 || this.currentIndex >= this.messages.length) {
        this.currentIndex = this.messages.length - 1;
    }

    return this.messages[this.currentIndex];
}

MessageQueue.prototype.Next = function () {
    this.currentIndex = this.currentIndex + 1;
    if (this.currentIndex >= this.messages.length || this.currentIndex < 0) {
        this.currentIndex = 0;
    }

    return this.messages[this.currentIndex];
}

MessageQueue.prototype.Add = function (message) {
    if (this.messages.length === this.capacity) {
        this.messages.shift();
    }
    this.messages.push(message);
    this.currentIndex = this.capacity - 1;
}