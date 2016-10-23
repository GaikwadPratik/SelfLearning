class NodeClass {
    constructor(nextObject, value) {
        this.value = null;
        this.next = null;
        this.next = nextObject;
        this.value = value;
    }
}
class LinkedList {
    constructor() {
        this._headNode = null;
    }
    isEmpty() {
        return this._headNode === null;
    }
    size() {
        let _rtnVal = 0;
        let _current = this._headNode;
        try {
            while (_current !== null) {
                _rtnVal++;
                _current = _current.next;
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
    prepend(value) {
        try {
            let _newNode = new NodeClass(this._headNode, value);
            this._headNode = _newNode;
        }
        catch (exception) {
            console.error(exception);
        }
    }
    append(value) {
        try {
            let _newNode = new NodeClass(null, value);
            if (this.isEmpty()) {
                this._headNode = _newNode;
            }
            else {
                let _current = this._headNode;
                while (_current.next != null) {
                    _current = _current.next;
                }
                _current.next = _newNode;
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }
    contains(value) {
        let _bRtnVal = false;
        try {
            let _current = this._headNode;
            while (_current !== null) {
                if (_current.value === value) {
                    _bRtnVal = true;
                    break;
                }
                else {
                    _current = _current.next;
                }
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _bRtnVal;
    }
    remove(value) {
        try {
            if (this.contains(value)) {
                if (this._headNode.value === value) {
                    this._headNode = this._headNode.next;
                }
                else {
                    let _previous = null;
                    let _current = this._headNode;
                    while (_current.value !== value) {
                        _previous = _current;
                        _current = _current.next;
                    }
                    _previous.next = _current.next;
                }
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }
    print() {
        let _outPut = '[';
        let _current = this._headNode;
        while (_current !== null) {
            _outPut += _current.value;
            if (_current.next !== null) {
                _outPut += ', ';
            }
            _current = _current.next;
        }
        _outPut += ']';
        console.log(_outPut);
    }
    addAt(nodeIndex, value) {
        try {
            if (nodeIndex > 0 && nodeIndex < this.size()) {
                let _current = this._headNode;
                for (let index = 0; index < nodeIndex - 1; index++) {
                    _current = _current.next;
                }
                let _newNode = new NodeClass(_current.next, value);
                _current.next = _newNode;
            }
            else
                console.error(`Index ${nodeIndex} is out of range`);
        }
        catch (exception) {
        }
    }
    indexOf(value) {
        let _nRtnVal = -1;
        let _bfound = false;
        try {
            let _current = this._headNode;
            while (_current !== null) {
                _nRtnVal++;
                if (_current.value !== value) {
                    _current = _current.next;
                }
                else {
                    _bfound = true;
                    break;
                }
            }
            if (!_bfound)
                _nRtnVal = -1;
        }
        catch (exception) {
            console.error(exception);
        }
        return _nRtnVal;
    }
    nodeAt(nodeIndex) {
        let _nodeRtn = this._headNode;
        try {
            if (nodeIndex > 0 && nodeIndex < this.size()) {
                for (let index = 0; index < nodeIndex - 1; index++) {
                    _nodeRtn = _nodeRtn.next;
                }
            }
            else
                console.error(`Index ${nodeIndex} is out of range`);
        }
        catch (exception) {
            console.error(exception);
        }
        return _nodeRtn;
    }
    addAfter(node, value) {
        try {
        }
        catch (exception) {
        }
    }
    bubbleSort() {
        try {
            let _tempSize = this.size();
            if (_tempSize > 1) {
                let _anyChanges = false;
                do {
                    let _current = this._headNode;
                    let _previous = null;
                    let _next = this._headNode.next;
                    _anyChanges = false;
                    while (_next !== null) {
                        this.print();
                        if (_current.value > _next.value) {
                            _anyChanges = true;
                            if (_previous !== null) {
                                let _tempNode = _next.next;
                                _previous.next = _next;
                                _next.next = _current;
                                _current.next = _tempNode;
                            }
                            else {
                                let _tempNode = _next.next;
                                this._headNode = _next;
                                _next.next = _current;
                                _current.next = _tempNode;
                            }
                            _previous = _next;
                            _next = _current.next;
                        }
                        else {
                            _previous = _current;
                            _current = _next;
                            _next = _next.next;
                        }
                    }
                } while (_anyChanges);
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }
}
let list = new LinkedList();
list.prepend(2);
list.prepend(4);
list.prepend(6);
list.append(100);
list.addAt(3, 50);
list.bubbleSort();
//# sourceMappingURL=app.js.map