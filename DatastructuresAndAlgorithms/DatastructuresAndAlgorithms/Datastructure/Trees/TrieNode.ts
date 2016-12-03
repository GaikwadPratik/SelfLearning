import { Dictionary } from '../Dictionary/Dictionary';

export class TrieNode {
    private _character: string = '';
    private _childNodes: Dictionary<string, TrieNode> = null;
    private _wordCount: number = 0;

    public get Character() {
        return this._character;
    }

    public set Character(value: string) {
        this._character = value;
    }

    public get ChildNodes() {
        return this._childNodes;
    }

    public set ChildNodes(value: Dictionary<string, TrieNode>) {
        this._childNodes = value;
    }

    public get WordCount() {
        return this._wordCount;
    }

    public set WordCount(value: number) {
        this._wordCount = value;
    }

    constructor(character: string, children: Dictionary<string, TrieNode>, wordCount: number) {
        this._character = character;
        this._childNodes = children;
        this._wordCount = wordCount;
    }

    public GetChildren(): Array<TrieNode> {
        let _rtnVal: Array<TrieNode> = [];
        try {
            _rtnVal = this._childNodes.Values;
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public GetChild(character: string): TrieNode {
        let _rtnVal: TrieNode = null;
        try {
            _rtnVal = this._childNodes.GetValue(character);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public SetChild(node: TrieNode): void {
        try {
            this._childNodes.SetValue(node.Character, node);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public RemoveChild(character: string): void {
        try {
            this._childNodes.Remove(character);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public Clear(): void {
        try {
            this._wordCount = 0;
            this._childNodes.Clear();
        }
        catch (exception) {
            console.error(exception);
        }
    }
}