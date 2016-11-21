export class LinkedListNode {
    constructor(data) {
        this._data = null;
        this._nextNode = null;
        this._previousNode = null;
        this._data = data;
    }
    get Data() {
        return this._data;
    }
    set Data(value) {
        this._data = value;
    }
    get NextNode() {
        return this._nextNode;
    }
    set NextNode(nodeValue) {
        this._nextNode = nodeValue;
    }
    get PreviousNode() {
        return this._previousNode;
    }
    set PreviousNode(nodeValue) {
        this._previousNode = nodeValue;
    }
}
//# sourceMappingURL=LinkedListNode.js.map