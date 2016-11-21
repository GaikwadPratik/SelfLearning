import { BaseCollection } from '../Common/Basecollection';
export class Queue extends BaseCollection {
    Enqueue(element) {
        try {
            this.Push(element);
        }
        catch (exception) {
            console.error(exception);
        }
    }
    Dequeue() {
        let _rtnVal = null;
        try {
            if (this.Length > 0)
                _rtnVal = this.Items.shift();
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
    Front() {
        let _rtnVal = null;
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
//# sourceMappingURL=Queue.js.map