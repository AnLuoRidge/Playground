/**
 * @param {string} s for string
 * @param {string} p for pattern
 * @return {boolean}
 */

    // split p to chars
    // create a resultFlags:array which length = s
    // split the pattern to ps: array
    // type: 1 step: a, ., multiple steps: .*, a*
    // loop ps each
    // for each p, decide step
    // log out steps
    // log out resultFlags

/**
    - reverse building: bottom to top
    - map, reduce, filter, foreach
    - DEBUG, RELEASE logs

*/
   
var isMatch = (s, p) => {
    // create a empty or with sth length-defined array
    // var foo = new Array(45); // create an empty array with length 45
    
    // const v = 0; // verbose

    var resultFlags = new Array(s.split('').length);
    resultFlags.forEach(flag => {
        flag = false;
    })
    
    var checkedPosition = -1;
    
    var parsePatterns = (p) => {
        // ab.c*d.*
        const parsers = p.split('');
        var patternArray = [];
        for(i=0; i<parsers.split; i++) {
            if (parser[i] != '.' && parser[i] != '*') {
                
            }
        }
        console.log('Parsed pattern array: ', patternArray);
    }
    
    // const ALPHABET = 0;
    // const SINGLE = 1;
    // const MULTIPLE = 2;
    // Mock parsedPatterns
    // const parsedPatterns = {'a*':}; 
    
    const pattern = 'ab.c*d.*';
    const parsers = p.split('');
    for(i=0; i<parsers.split; i++) {
        if (parser[i] != '.' && parser[i] != '*') {
            //
        }
    }
    
    
    // resultFlags = [false];
    console.log("Result flags: ", resultFlags);
    resultFlags.forEach(flag => {
        if (flag != true) {
            return false;
        }
    })
    return true;
};

const test = (cases) => {
  var passCount = 0;
  for (let key in cases) {
    const testCase = cases[key];
    console.log(`${testCase.s} | ${testCase.p}`);
    if (isMatch(testCase.s, testCase.p) === testCase.a) {
      console.log('[PASS]');
      console.log('---------------------------------------------')
      passCount ++;
    } else {
      console.log('[FAIL]');
      console.log('---------------------')
    }
  }
  const caseLength = Object.keys(cases).length;
  const passRate = Math.round(passCount / caseLength * 100);
  console.log(`PASS: ${passRate} %`)
}

const cases = {
  fullMatch: {s: 'aa', p: 'aa', a: true}, 
  fullMatch2: {s: 'aa', p: 'ab', a: false},
  dot: {s: 'aa', p: 'a.', a: true},
  asterisk: {s: 'me', p: 'me*', a: true}
};

// run test
test(cases);
