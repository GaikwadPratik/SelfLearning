import { Queue } from './Queue';

export class QueueElement<T>
{
    private _element: T = null;
    private _priority: number = 0;

    public get Element(): T {
        return this._element;
    }

    public get Priority(): number {
        return this._priority;
    }

    public set Element(element: T) {
        this._element = element;
    }

    public set Priority(priority: number) {
        this._priority = priority;
    }

    constructor(element: T, priority: number) {
        this._element = element;
        this._priority = priority;
    }
}

export class PriorityQueue<T> extends Queue<QueueElement<T>>
{
    public Enqueue(element: QueueElement<T>) {
        try {
            if (this.IsEmpty) {
                this.Enqueue(element);
            }
            else {
                let _bIsAdded: boolean = false;
                for (let _index = 0, len = this.Length; _index < len; _index++) {
                    //Since queue implement FIFO, new element must be at the last with same priority
                    if (element.Priority < this.Items[_index].Priority) {
                        this.Items.splice(_index, 0, element);
                        _bIsAdded = true;
                        break;
                    }
                }
                if (!_bIsAdded)
                    this.Enqueue(element);
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }
}