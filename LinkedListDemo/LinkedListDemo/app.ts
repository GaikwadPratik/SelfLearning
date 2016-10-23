enum Direction {
    forward,
    backword
}

interface INodeClass<T> {
    value: T;
    next: INodeClass<T>;
}
class NodeClass<T> implements INodeClass<T> {
    public value: T = null;
    public next: INodeClass<T> = null;
    constructor(nextObject: INodeClass<T>, value: T) {
        this.next = nextObject;
        this.value = value;
    }
}

class LinkedList<T>{
    private _headNode: INodeClass<T> = null;

    public isEmpty(): boolean {
        return this._headNode === null;
    }

    public size(): number {
        let _rtnVal: number = 0;
        let _current: INodeClass<T> = this._headNode;
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

    public prepend(value: T): void {
        try {
            let _newNode: INodeClass<T> = new NodeClass(this._headNode, value);
            this._headNode = _newNode;
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public append(value: T): void {
        try {
            let _newNode: INodeClass<T> = new NodeClass<T>(null, value);

            if (this.isEmpty()) {
                this._headNode = _newNode;
            }
            else {
                let _current: INodeClass<T> = this._headNode;
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

    public contains(value: T): boolean {
        let _bRtnVal: boolean = false;
        try {
            let _current: INodeClass<T> = this._headNode;
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

    public remove(value: T): void {
        try {
            if (this.contains(value)) {
                if (this._headNode.value === value) {
                    this._headNode = this._headNode.next;
                }
                else {
                    let _previous: INodeClass<T> = null;
                    let _current: INodeClass<T> = this._headNode;

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

    public print(): void {
        let _outPut: string = '[';
        let _current: INodeClass<T> = this._headNode;
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

    public addAt(nodeIndex: number, value: T): void {
        try {
            if (nodeIndex > 0 && nodeIndex < this.size()) {
                let _current: INodeClass<T> = this._headNode;
                for (let index = 0; index < nodeIndex - 1; index++) {
                    _current = _current.next;
                }
                let _newNode: INodeClass<T> = new NodeClass(_current.next, value);
                _current.next = _newNode;
            }
            else
                console.error(`Index ${nodeIndex} is out of range`);
        }
        catch (exception) {
        }
    }

    public indexOf(value: T): number {
        let _nRtnVal: number = -1;
        let _bfound: boolean = false;
        try {
            let _current: INodeClass<T> = this._headNode;
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

    private nodeAt(nodeIndex: number): INodeClass<T> {
        let _nodeRtn: INodeClass<T> = this._headNode;
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

    public addAfter(node: INodeClass<T>, value: T) {
        try {

        }
        catch (exception) {
        }
    }

    public bubbleSort(): void {
        try {
            let _tempSize = this.size();
            if (_tempSize > 1) {
                let _anyChanges: boolean = false;
                let _direction: Direction = Direction.forward;

                do {
                    let _current: INodeClass<T> = this._headNode;
                    let _previous: INodeClass<T> = null;
                    let _next: INodeClass<T> = this._headNode.next;
                    _anyChanges = false;

                    switch (_direction) {
                        case Direction.forward:
                            //while (_next !== null) {
                            if (_next !== null) {
                                if (_current.value > _next.value) {
                                    _anyChanges = true;
                                    if (_previous !== null) {
                                        let _tempNode: INodeClass<T> = _next.next;
                                        _previous.next = _next;
                                        _next.next = _current;
                                        _current.next = _tempNode;
                                    }
                                    else {
                                        let _tempNode: INodeClass<T> = _next.next;
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
                                _direction = Direction.backword;
                            }
                            //}
                            break;

                        case Direction.backword:

                            break;
                    }
                }
                while (_anyChanges);
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }
}


let list: LinkedList<number> = new LinkedList<number>();
console.log(list.isEmpty());
list.prepend(2);
list.print();
console.log(list.size());
list.prepend(4);
list.prepend(6);
console.log(list.indexOf(2));
console.log(list.indexOf(50));
list.print();
console.log(list.size());
console.log(list.isEmpty());
list.append(100);
list.print();
console.log(list.contains(100));
console.log(list.contains(7));
console.log(list.size());
list.addAt(3, 50);
list.print();
console.log(list.indexOf(50));
list.bubbleSort();
list.print();