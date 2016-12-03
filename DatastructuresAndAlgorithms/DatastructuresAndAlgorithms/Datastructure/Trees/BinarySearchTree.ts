import { BinarySearchTreeNode } from 'BinarySearchTreeNode';

export class BinarySearchTree<T> {
    private _rootNode: BinarySearchTreeNode<T> = null;
    private _nElements: number = 0;

    public get Count() {
        return this._nElements;
    }

    /**
    * Adds the specified element to this tree if it is not already present.
    * @param {Object} value the element to insert.
    */
    public Add(value: T): void {
        try {
            let _newNode = new BinarySearchTreeNode<T>(value);
            if (this._rootNode === null)
                this._rootNode = _newNode;
            else {
                this.InsertNode(this._rootNode, _newNode);
            }
            this._nElements++;
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private InsertNode(currentNode: BinarySearchTreeNode<T>, newNode: BinarySearchTreeNode<T>) {
        if (newNode.NodeValue < currentNode.NodeValue) {
            if (currentNode.LeftNode === null)
                currentNode.LeftNode = newNode;
            else
                this.InsertNode(currentNode.LeftNode, newNode);
        }
        else {
            if (currentNode.RightNode === null)
                currentNode.RightNode = newNode;
            else
                this.InsertNode(currentNode.RightNode, newNode);
        }
    }

    /**
     * Returns true if this tree contains the specified element.
     * @param {Object} value element to search for.
     * @return {boolean} true if this tree contains the specified element,
     * false otherwise.
     */
    public Contains(value: T): boolean {
        let _rtnVal: boolean = false;
        try {
            _rtnVal = this.SearchNode(this._rootNode, value);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    private SearchNode(node: BinarySearchTreeNode<T>, value: T): boolean {
        let _rtnVal: boolean = false;
        try {
            if (node !== null) {
                if (value < node.NodeValue)
                    _rtnVal = this.SearchNode(node.LeftNode, value);
                else if (value > node.NodeValue)
                    _rtnVal = this.SearchNode(node.RightNode, value);
                else
                    _rtnVal = true;
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    /**
     * Executes the provided function once for each element present in this tree in
     * in-order.
     * @param {function(Object):*} callback function to execute, it is invoked with one
     * argument: the element value, to break the iteration you can optionally return false.
     */
    public InOrderTraverse(callBack: Function): void {
        try {
            this.InOrderTraverseNode(this._rootNode, callBack);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private InOrderTraverseNode(currentNode: BinarySearchTreeNode<T>, callBack: Function): void {
        try {
            if (currentNode !== null) {
                this.InOrderTraverseNode(currentNode.LeftNode, callBack);
                callBack(currentNode.NodeValue);
                this.InOrderTraverseNode(currentNode.RightNode, callBack);
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }

    /**
    * Executes the provided function once for each element present in this tree in pre-order.
    * @param {function(Object):*} callback function to execute, it is invoked with one
    * argument: the element value, to break the iteration you can optionally return false.
    */
    public PreOrderTraverse(callBack: Function): void {
        try {
            this.PreOrderTraverseNode(this._rootNode, callBack);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private PreOrderTraverseNode(currentNode: BinarySearchTreeNode<T>, callBack: Function): void {
        try {
            if (currentNode !== null) {
                callBack(currentNode.NodeValue);
                this.PreOrderTraverseNode(currentNode.LeftNode, callBack);
                this.PreOrderTraverseNode(currentNode.RightNode, callBack);
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }

    /**
     * Executes the provided function once for each element present in this tree in post-order.
     * @param {function(Object):*} callback function to execute, it is invoked with one
     * argument: the element value, to break the iteration you can optionally return false.
     */
    public PostOrderTraverse(callBack: Function): void {
        try {
            this.PostOrderTraverseNode(this._rootNode, callBack);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private PostOrderTraverseNode(currentNode: BinarySearchTreeNode<T>, callBack: Function): void {
        try {
            if (currentNode !== null) {
                this.PostOrderTraverseNode(currentNode.LeftNode, callBack);
                this.PostOrderTraverseNode(currentNode.RightNode, callBack);
                callBack(currentNode.NodeValue);
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }

    /**
    * Returns the minimum element of this tree.
    * @return {*} the minimum element of this tree or undefined if this tree is
    * is empty.
    */
    public MinimumValue(): T | null {
        let _rtnVal: T = null;
        try {
            let _minNode = this.MinimumNode(this._rootNode);
            if (_minNode && _minNode !== null)
                _rtnVal = _minNode.NodeValue;
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    private MinimumNode(currentNode: BinarySearchTreeNode<T>): BinarySearchTreeNode<T> | null {
        let _rtnVal: BinarySearchTreeNode<T> = null;
        try {
            if (currentNode) {
                while (currentNode && currentNode.LeftNode !== null)
                    currentNode = currentNode.LeftNode;
                _rtnVal = currentNode;
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    /**
     * Returns the maximum element of this tree.
     * @return {*} the maximum element of this tree or undefined if this tree is
     * is empty.
     */
    public MaximumValue(): T | null {
        let _rtnVal: T = null;
        try {
            let _maxNode = this.MaximumNode(this._rootNode);
            if (_maxNode && _maxNode != null)
                _rtnVal = _maxNode.NodeValue;
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    private MaximumNode(currentNode: BinarySearchTreeNode<T>): BinarySearchTreeNode<T> | null {
        let _rtnVal: BinarySearchTreeNode<T> = null;
        try {
            if (currentNode) {
                while (currentNode && currentNode.RightNode !== null)
                    currentNode = currentNode.RightNode;
                _rtnVal = currentNode;
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    /**
     * Removes the specified element from this tree if it is present.
     * @return {boolean} true if this tree contained the specified element.
     */
    public Delete(value: T): boolean {
        let _rtnVal: boolean = false;
        try {
            this.DeleteNode(this._rootNode, value);
            this._nElements--;
            _rtnVal = true;
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    private DeleteNode(currentNode: BinarySearchTreeNode<T>, value: T): BinarySearchTreeNode<T> {
        try {
            if (currentNode !== null) {//value is less than currnent node
                if (value < currentNode.NodeValue) {
                    currentNode.LeftNode = this.DeleteNode(currentNode.LeftNode, value);
                    return currentNode;
                }
                else if (value > currentNode.NodeValue) {//value is greater than currnent node
                    currentNode.RightNode = this.DeleteNode(currentNode.RightNode, value);
                    return currentNode;
                }
                else {//value is equal to currnent node
                    //handle 3 special conditions
                    //1 - a leaf node
                    //2 - a node with only 1 child
                    //3 - a node with 2 children

                    //case 1
                    if (currentNode.LeftNode === null && currentNode.RightNode === null) {
                        currentNode = null;
                        return currentNode;
                    }
                    //case 2
                    else if (currentNode.LeftNode === null) {
                        currentNode = currentNode.RightNode;
                        return currentNode;
                    }
                    else if (currentNode.RightNode === null) {
                        currentNode = currentNode.LeftNode;
                        return currentNode;
                    }


                    //case 3
                    let _minNode = this.MinimumNode(currentNode.RightNode);
                    currentNode.NodeValue = _minNode.NodeValue;
                    currentNode.RightNode = this.DeleteNode(currentNode.RightNode, _minNode.NodeValue);
                    return currentNode;
                }
            }
        }
        catch (exception) {
            throw new Error(exception);
        }
    }
}