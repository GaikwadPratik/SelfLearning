export interface IKeyValuePair<TKey, TValue> {
    key: TKey;
    value: TValue;
}

export class Dictionary<TKey, TValue>
{
    constructor() {
        this._items = {};
    }

    private _items: Object = null;
    private _nElement: number = 0;

    public get Count() {
        return this._nElement;
    }

    public get Keys() {
        let _rtnVal: Array<TKey> = [];
        try {
            for (let _keyName in this._items) {
                if (this._items.hasOwnProperty(_keyName)) {
                    let _element: IKeyValuePair<TKey, TValue> = this._items[_keyName];
                    if (_element) {
                        _rtnVal.push(_element.key);
                    }
                }
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public get Values() {
        let _rtnVal: Array<TValue> = [];
        try {
            for (let _keyName in this.Keys) {
                if (this._items.hasOwnProperty(_keyName)) {
                    let _element: IKeyValuePair<TKey, TValue> = this._items[_keyName];
                    if (_element) {
                        _rtnVal.push(_element.value);
                    }
                }
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    private GenerateKey(key: any): string {
        let _rtnVal: string = '';
        try {
            if (key) {
                if (Object.prototype.toString.call(key) === '[object String]')
                    _rtnVal = '_sDic' + key;
                else
                    _rtnVal = '_oDic' + key.toString();
            }
            else
                console.error(`${key} is undefined`);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public ContainsKey(key: TKey): boolean {
        let _rtnVal: boolean = false;
        try {
            let _strGeneratedKey: string = this.GenerateKey(key);
            if (_strGeneratedKey !== '') {
                _rtnVal = this._items.hasOwnProperty(_strGeneratedKey);
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public SetValue(key: TKey, value: TValue): void {
        if (key && value) {
            let _strGeneratedKey: string = this.GenerateKey(key);
            if (_strGeneratedKey !== '') {
                if (!this.ContainsKey(key)) {
                    let _element: IKeyValuePair<TKey, TValue> = this._items[_strGeneratedKey];
                    if (!_element) {
                        this._nElement++;
                        this._items[_strGeneratedKey] = value;
                    }
                }
                else
                    throw new Error(`${key} already exists in the collection`);
            }
            else
                console.warn(`An error occurred while generating key in the dictionary`);
        }
        else
            console.log('either key or value is undefined.');
    }

    public GetValue(key: TKey): TValue {
        let _rtnVal: TValue = null;
        try {
            if (key) {
                let _strGeneratedKey: string = this.GenerateKey(key);
                if (_strGeneratedKey !== '') {
                    if (this.ContainsKey(key)) {
                        let _element: IKeyValuePair<TKey, TValue> = this._items[_strGeneratedKey];
                        if (_element) {
                            _rtnVal = _element.value;
                        }
                        else
                            console.debug(`Element with ${key} not found`);
                    }
                    else
                        throw new Error(`${key} not found in the collection`);
                }
                else
                    console.warn(`An error occurred while generating key in the dictionary`);
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public Remove(key: TKey): boolean {
        let _rtnVal: boolean = false;
        try {
            if (this.ContainsKey(key)) {
                let _strGeneratedKey: string = this.GenerateKey(key);
                if (_strGeneratedKey !== '') {
                    delete this._items[_strGeneratedKey];
                    this._nElement--;
                    _rtnVal = true;
                }
            }
            else
                throw new Error(`${key} is not present in the collection`);
        }
        catch (exception) {
            _rtnVal = false;
            console.error(exception);
        }
        return _rtnVal;
    }

    public Clear(): void {
        try {
            this._items = {};
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public ForEach(callback: (key: TKey, value: TValue) => any): void {
        for (let _keyName in this._items) {
            if (this._items.hasOwnProperty(_keyName)) {
                let _element: IKeyValuePair<TKey, TValue> = this._items[_keyName];
                const ret = callback(_element.key, _element.value);
                if (ret === false) {
                    return;
                }
            }
        }
    }
}