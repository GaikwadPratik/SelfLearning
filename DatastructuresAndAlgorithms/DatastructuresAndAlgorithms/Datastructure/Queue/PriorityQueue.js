import { Queue } from './Queue';
export class QueueElement {
    constructor(element, priority) {
        this._element = null;
        this._priority = 0;
        this._element = element;
        this._priority = priority;
    }
    get Element() {
        return this._element;
    }
    get Priority() {
        return this._priority;
    }
    set Element(element) {
        this._element = element;
    }
    set Priority(priority) {
        this._priority = priority;
    }
}
export class PriorityQueue extends Queue {
    Enqueue(element) {
        try {
            if (this.IsEmpty) {
                this.Enqueue(element);
            }
            else {
                let _bIsAdded = false;
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
//# sourceMappingURL=PriorityQueue.js.map