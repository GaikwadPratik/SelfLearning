export class LinkedListNode<T>
{
    private _data: T = null;
    private _nextNode: LinkedListNode<T> = null;
    private _previousNode: LinkedListNode<T> = null;

    public get Data(): T {
        return this._data;
    }
    public set Data(value: T) {
        this._data = value;
    }

    public get NextNode(): LinkedListNode<T> {
        return this._nextNode;
    }

    public set NextNode(nodeValue: LinkedListNode<T>) {
        this._nextNode = nodeValue;
    }

    public get PreviousNode(): LinkedListNode<T> {
        return this._previousNode;
    }

    public set PreviousNode(nodeValue: LinkedListNode<T>) {
        this._previousNode = nodeValue;
    }

    constructor(data: T) {
        this._data = data;
    }
}