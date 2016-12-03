export class BinarySearchTreeNode<T>{
    private _nodeValue: T = null;
    private _leftNode: BinarySearchTreeNode<T> = null;
    private _rightNode: BinarySearchTreeNode<T> = null;

    public get NodeValue(){
        return this._nodeValue;
    }

    public set NodeValue(value: T) {
        this._nodeValue = value;
    }

    public get LeftNode() {
        return this._leftNode;
    }

    public set LeftNode(value: BinarySearchTreeNode<T>) {
        this._leftNode = value;
    }

    public get RightNode() {
        return this._rightNode;
    }

    public set RightNode(value: BinarySearchTreeNode<T>) {
        this._rightNode = value;
    }

    constructor(value: T) {
        this._nodeValue = value;
    }
}