import { BaseCollection } from '../Common/Basecollection';

export class Queue<T> extends BaseCollection<T>
{
    public Enqueue(element: T): void {
        try {
            this.Push(element);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public Dequeue(): T | null {
        let _rtnVal: T = null;
        try {
            if (this.Length > 0)
                _rtnVal = this.Items.shift();
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public Front(): T | null {
        let _rtnVal: T = null;
        try {
            if (this.Length > 0)
                _rtnVal = this.Items[0];
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
}