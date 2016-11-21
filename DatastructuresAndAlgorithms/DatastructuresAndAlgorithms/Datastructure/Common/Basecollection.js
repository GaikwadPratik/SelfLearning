export class BaseCollection {
    constructor() {
        this._items = null;
        this._items = new Array();
    }
    get Items() {
        return this._items;
    }
    set Items(arr) {
        this._items = arr;
    }
    get Length() {
        return this._items.length;
    }
    get IsEmpty() {
        return this.Length === 0;
    }
    Push(elem) {
        try {
            this._items.push(elem);
        }
        catch (exception) {
            console.error(exception);
        }
    }
    Clear() {
        try {
            this._items = new Array();
        }
        catch (exception) {
            console.error(exception);
        }
    }
    Pop() {
        let _rtnVal = null;
        try {
            _rtnVal = this.Items.pop();
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }
    Print() {
        console.log(this.Items.toString());
    }
}
//# sourceMappingURL=Basecollection.js.map