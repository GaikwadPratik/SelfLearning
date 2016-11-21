import { BaseCollection } from '../Common/Basecollection';

export class Stack<T> extends BaseCollection<T>
{
    public Push(elem: T): void {
        try {
            this.Push(elem);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public Pop(): T | null {
        let _rtnVal: T = null;
        try {
            _rtnVal = this.Pop();
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public Peek(): T | null {
        let _rtnVal: T = null;
        try {
            if (this.Length > 0)
                _rtnVal = this.Items[this.Length - 1];
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
}