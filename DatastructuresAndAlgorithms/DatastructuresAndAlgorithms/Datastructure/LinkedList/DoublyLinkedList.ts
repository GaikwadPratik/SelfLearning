import { LinkedListNode } from 'LinkedListNode';

export class DoublyLinkedList<T>
{
    private _root: LinkedListNode<T> = null;
    //private _lastNode: LinkedListNode<T> = null;
    private _length: number = 0;

    public get Root() {
        return this._root;
    }

    public get Length() {
        return this._length;
    }

    public IsEmpty() {
        return this._length === 0;
    }

    public Add(data: T): void {
        try {
            let _newNode: LinkedListNode<T> = new LinkedListNode<T>(data);
            let _currentNode: LinkedListNode<T> = null;

            if (this._root === null) {
                this._root = _newNode;
                //this._lastNode = _newNode;
            }
            else {
                _currentNode = this._root;

                while (_currentNode.NextNode !== null)
                    _currentNode = _currentNode.NextNode;

                _currentNode.NextNode = _newNode;
                _newNode.PreviousNode = _currentNode;
            }

            this._length++;
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public InsertAt(data: T, position: number): boolean {
        let _rtnVal: boolean = false;
        try {
            if (position >= 0 && position <= length) {
                let _newNode: LinkedListNode<T> = new LinkedListNode<T>(data);
                let _currentNode: LinkedListNode<T> = this._root;
                let _previousNode: LinkedListNode<T> = null;
                let _index: number = 0;

                if (position === 0) {
                    _newNode.NextNode = _currentNode;
                    this._root.PreviousNode = _newNode;
                    this._root = _newNode;
                }
                else {
                    while (_index++ < position) {
                        _previousNode = _currentNode;
                        _currentNode = _currentNode.NextNode;
                    }

                    _newNode.NextNode = _currentNode;
                    _previousNode.NextNode = _newNode;

                    _currentNode.PreviousNode = _newNode;
                    _newNode.PreviousNode = _previousNode
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

    public IndexOf(data: T): number {
        let _rtnVal: number = -1;
        try {
            let _currentNode: LinkedListNode<T> = this._root;
            let _index: number = 0;

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

    public RemoveAt(position: number): T | null {
        let _rtnVal: T = null;
        try {
            if (position >= 0 && position <= this._length) {
                let _currentNode: LinkedListNode<T> = this._root;
                let _previousNode: LinkedListNode<T> = null;
                let _index: number = 0;
                if (position === 0)
                    this._root = _currentNode.NextNode;
                else {
                    while (_index++ < position) {
                        _previousNode = _currentNode;
                        _currentNode = _currentNode.NextNode;
                    }

                    _previousNode.NextNode = _currentNode.NextNode;

                    _previousNode.NextNode = _currentNode.NextNode;
                    _currentNode.NextNode.PreviousNode = _previousNode;
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

    public Remove(data: T): T | null {
        let _rtnVal: T = null;
        try {
            let _index: number = this.IndexOf(data);
            if (_index !== -1)
                _rtnVal = this.RemoveAt(_index);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
}