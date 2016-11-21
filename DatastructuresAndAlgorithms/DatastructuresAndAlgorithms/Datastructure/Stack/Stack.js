import { BaseCollection } from '../Common/Basecollection';
export class Stack extends BaseCollection {
    Push(elem) {
        try {
            this.Push(elem);
        }
        catch (exception) {
            console.error(exception);
        }
    }
    Pop() {
        let _rtnVal = null;
        try {
            _rtnVal = this.Pop();
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
    Peek() {
        let _rtnVal = null;
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
//# sourceMappingURL=Stack.js.map