export class BaseCollection<T> {
    private _items: Array<T> = null;

    constructor() {
        this._items = new Array<T>();
    }

    public get Items(): Array<T> {
        return this._items;
    }

    public set Items(arr: Array<T>) {
        this._items = arr;
    }

    public get Length(): number {
        return this._items.length;
    }

    public get IsEmpty(): boolean {
        return this.Length === 0;
    }

    protected Push(elem: T): void {
        try {
            this._items.push(elem);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public Clear(): void {
        try {
            this._items = new Array<T>();
        }
        catch (exception) {
            console.error(exception);
        }
    }

    protected Pop(): T | null {
        let _rtnVal: T = null;
        try {
            _rtnVal = this.Items.pop();
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public Print() {
        console.log(this.Items.toString());
    }
}