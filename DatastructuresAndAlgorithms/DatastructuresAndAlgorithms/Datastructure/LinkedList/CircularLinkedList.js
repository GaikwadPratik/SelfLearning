import { LinkedListNode } from 'LinkedListNode';
export class CircularLinkedList {
    constructor() {
        this._root = null;
        this._length = 0;
    }
    get Root() {
        return this._root;
    }
    get Length() {
        return this._length;
    }
    IsEmpty() {
        return this._length === 0;
    }
    Add(data) {
        try {
            let _newNode = new LinkedListNode(data);
            let _currentNode = null;
            if (this._root === null)
                this._root = _newNode;
            else {
                _currentNode = this._root;
                while (_currentNode.NextNode !== this._root)
                    _currentNode = _currentNode.NextNode;
                _currentNode.NextNode = _newNode;
            }
            _newNode.NextNode = this._root;
            this._length++;
        }
        catch (exception) {
            console.error(exception);
        }
    }
    InsertAt(data, position) {
        let _rtnVal = false;
        try {
            if (position >= 0 && position <= length) {
                let _newNode = new LinkedListNode(data);
                let _currentNode = this._root;
                let _previousNode = null;
                let _index = 0;
                if (position === 0) {
                    _newNode.NextNode = _currentNode;
                    while (_currentNode.NextNode !== this._root)
                        _currentNode = _currentNode.NextNode;
                    _currentNode.NextNode = _newNode;
                }
                else {
                    while (_index++ < position) {
                        _previousNode = _currentNode;
                        _currentNode = _currentNode.NextNode;
                    }
                    _newNode.NextNode = _currentNode;
                    _previousNode.NextNode = _newNode;
                    if (_newNode.NextNode === null)
                        _newNode.NextNode = this._root;
                }
                this._length++;
                _rtnVal = true;
            }
            else
                console.error(`${position} is outside bounds of the list`);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
    IndexOf(data) {
        let _rtnVal = -1;
        try {
            let _currentNode = this._root;
            let _index = 0;
            while (_currentNode !== null) {
                if (_currentNode.Data === data) {
                    _rtnVal = _index;
                    break;
                }
                else {
                    _index++;
                    _currentNode = _currentNode.NextNode;
                }
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
    RemoveAt(position) {
        let _rtnVal = null;
        try {
            if (position >= 0 && position <= this._length) {
                let _currentNode = this._root;
                let _previousNode = null;
                let _index = 0;
                if (position === 0) {
                    while (_currentNode.NextNode !== this._root) {
                        _currentNode = _currentNode.NextNode;
                    }
                    this._root = _currentNode.NextNode;
                    _currentNode.NextNode = this._root;
                }
                else {
                    while (_index++ < position) {
                        _previousNode = _currentNode;
                        _currentNode = _currentNode.NextNode;
                    }
                    _previousNode.NextNode = _currentNode.NextNode;
                }
                this._length--;
                _rtnVal = _currentNode.Data;
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
    Remove(data) {
        let _rtnVal = null;
        try {
            let _index = this.IndexOf(data);
            if (_index !== -1)
                _rtnVal = this.RemoveAt(_index);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
}
//# sourceMappingURL=CircularLinkedList.js.map