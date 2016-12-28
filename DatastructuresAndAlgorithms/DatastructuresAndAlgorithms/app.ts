//function StairCase(n) {
//    if (1 <= n && n < 100) {
//        for (var i = 1; i <= n; i++) {
//            console.log(' '.repeat(n - i) + '#'.repeat(i));
//        }
//    }
//    else {
//        console.log(`input must be between 1 and 100`);
//    }

//}

//StairCase(5);

//function sum(numbers) {
//    let size = numbers[0];
//    let sum = 0;
//    for (let index = 1; index <= size; index++)
//        sum = sum + numbers[index];
//    console.log(sum);
//}

//sum([5, 1, 2, 3, 4, 5]);

//function main() {
//    var a = 'cde';
//    var b = 'abc';
//    var c = 0;
//    for (let index = 0; index < a.length; index++) {
//        for (let innerIndex = 0; innerIndex < b.length; innerIndex++) {
//            if (a[index] === b[innerIndex]) {
//                c++;
//                break;
//            }
//        }
//    }
//    console.log(a.length + b.length - (2*c));
//}

//main();

//function Checkavailability(magzine: Array<string>, ransom: Array<string>) {
//    let dictionary1 = {};
//    magzine.forEach(function (value, index, array) {
//        if (!dictionary1.hasOwnProperty(value)) {
//            dictionary1[value] = 1;
//        }
//        else
//            dictionary1[value] += 1;
//    });

//    ransom.forEach(function (value, index, array) {
//        if (!dictionary1.hasOwnProperty(value))
//            dictionary1[value] = 0;
//        else if (dictionary1.hasOwnProperty(value) && dictionary1[value] === 0)
//            return false;
//        else
//            dictionary1[value] -= 1;
//    });
//    return true;
//}

//function IsBalancedExpression(expression) {
//    let arr = [];
//    for (let index = 0; index < expression.length; index ++){
//        if (expression[index] === '{')
//            arr.push('}');
//        else if (expression[index] === '[')
//            arr.push(']');
//        else if (expression[index] === '(')
//            arr.push(')');
//        else {
//            if (arr.length === 0 || expression[index] !== arr.pop())
//                return false;
//        }
//    }
//    return arr.length === 0;
//}

//function counting(s) {
//    let count = 0;
//    for (let n = 1; n < s.length / 2; n++) {
//        for (let index = 0; index < s.length; index++) {
//            let firststring = s.substring(index, index + n);
//            let secondstring = s.substring(index + n, index + (2 * n));
//            let runFirststring = firststring && (firststring.length === 1 || /^([a-zA-Z0-9])\1+$/.test(firststring));
//            let runSecondstring = secondstring && (secondstring.length === 1 || /^([a-zA-Z0-9])\1+$/.test(secondstring));
            
//            if (firststring
//                && secondstring
//                && firststring !== ''
//                && secondstring !== ''
//                && runFirststring
//                && runSecondstring
//                && firststring.length === secondstring.length
//                && firststring !== secondstring)
//                count++;
//            else
//                continue;
//        }
//    }

//    return count;
//}
//console.log(counting('00110'));//3
//console.log(counting('10101'));//4
//console.log(counting('10001'));//2