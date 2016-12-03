import { TrieNode } from 'TrieNode';
import { Dictionary } from '../Dictionary/Dictionary';

export interface ITrie {
    readonly TotalWords: number;
    AddWord(word: string): void;
    RemoveWord(word: string): void;
    RemovePrefix(prefix: string): void;
    GetAllWords(): Array<string>;
    GetWordsByPrefix(prefix: string): Array<string>;
    ContainsWord(word: string): boolean;
    Clear(): void;
}

export class TrieFactory {
    /// <summary>
    /// To create an instance of Trie object
    /// </summary>
    /// <returns></returns>
    public static CreateTrie(): ITrie {
        return new Trie(this.CreateTrieNode(" "));
    }

    public static CreateTrieNode(character: string): TrieNode {
        return new TrieNode(character, new Dictionary<string, TrieNode>(), 0);
    }
}

class Trie implements ITrie {
    private _rootNode: TrieNode = null;

    constructor(rootNode: TrieNode) {
        this._rootNode = rootNode;
    }

    public get TotalWords() {
        let _rtnVal: number = 0;
        try {
            this.GetCount(this._rootNode, _rtnVal);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public AddWord(word: string): void {
        try {
            this.AddWordNode(this._rootNode, word);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public Clear(): void {
        try {
            this._rootNode.Clear();
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public ContainsWord(word: string): boolean {
        let _rtnVal: boolean = false;
        try {
            let _node: TrieNode = this.GetNode(word);
            _rtnVal = _node !== null;
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public GetAllWords(): Array<string> {
        let _rtnVal: Array<string> = [];
        try {
            let _tempString: string = '';
            this.GetAllWordNodes(this._rootNode, _rtnVal, _tempString);
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public GetWordsByPrefix(prefix: string): Array<string> {
        let _rtnVal: Array<string> = [];
        try {
            let _node: TrieNode = this.GetNode(prefix);
            if (_node !== null) {
                let _tempString: string = '';
                this.GetAllWordNodes(this._rootNode, _rtnVal, _tempString);
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    public RemovePrefix(prefix: string): void {
        try {
            let _nodes: Array<TrieNode> = this.GetTrieNodes(prefix, false);
            if (_nodes.length > 0) {
                let _lastNode: TrieNode = _nodes.pop();
                if (_lastNode !== null)
                    _lastNode.Clear();
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }

    public RemoveWord(word: string): void {
        try {
            let _nodes: Array<TrieNode> = this.GetTrieNodes(word, false);
            if (_nodes.length > 0)
                this.RemoveWords(_nodes);
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private AddWordNode(node: TrieNode, word: string): void {
        try {
            for (let _index = 0, len = word.length; _index < len; _index++) {
                let _charToAdd: string = word[_index];
                let _childNode: TrieNode = node.GetChild(_charToAdd);
                if (_childNode === null) {
                    _childNode = TrieFactory.CreateTrieNode(_charToAdd);
                    node.SetChild(_childNode);
                }
                node = _childNode;
            }
            node.WordCount++;
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private GetAllWordNodes(node: TrieNode, words: Array<string>, tempString: string): void {
        try {
            if (node.WordCount > 0) {
                words.push(tempString);
            }

            node.ChildNodes.Values.forEach((child: TrieNode) => {
                tempString += child.Character;
                this.GetAllWordNodes(child, words, tempString);
                tempString = tempString.slice(0, -1);
            });
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private GetCount(node: TrieNode, count: number): void {
        try {
            if (node.WordCount > 0)
                count += node.WordCount;
            node.ChildNodes.Values.forEach((childNode: TrieNode) => {
                this.GetCount(childNode, count);
            });
        }
        catch (exception) {
            console.error(exception);
        }
    }

    private GetNode(word: string): TrieNode {
        let _rtnVal: TrieNode = this._rootNode;
        try {
            for (let w of word) {
                _rtnVal = _rtnVal.GetChild(w);
                if (_rtnVal === null)
                    break;
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    private GetTrieNodes(s: string, isWord: boolean = true): Array<TrieNode> {
        let _rtnVal: Array<TrieNode> = [];
        try {
            let _trieNode: TrieNode = this._rootNode;
            _rtnVal.push(_trieNode);
            for (let c of s) {
                _trieNode = _trieNode.GetChild(c);
                if (_trieNode === null) {
                    _rtnVal = [];
                    break;
                }
                _rtnVal.push(_trieNode);
            }

            if (isWord) {
                if (_trieNode === null || _trieNode.WordCount === 0)
                    console.error(`${s} doesn't exist in trie`);
            }
        }
        catch (exception) {
            console.error(exception);
        }
        return _rtnVal;
    }

    private RemoveWords(nodes: Array<TrieNode>): void {
        try {
            let _lastNode: TrieNode = nodes.pop();
            if (_lastNode !== null)
                _lastNode.WordCount = 0;
            while (nodes.length > 1) {
                let _secondLastElement: TrieNode = nodes.pop();
                if (_lastNode.WordCount > 0 || _lastNode.ChildNodes.Count > 0)
                    break;
                _secondLastElement.RemoveChild(_lastNode.Character);
                _lastNode = _secondLastElement;
            }
        }
        catch (exception) {
            console.error(exception);
        }
    }
}