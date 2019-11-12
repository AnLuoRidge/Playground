/**
 * @param {string} s for string
 * @param {string} p for pattern
 * @return {boolean}
 */

    // func parseWildCards
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
    
    var checkingPosition = -1;
    
    // const ALPHABET = 0;
    // const SINGLE = 1;
    // const MULTIPLE = 2;
    // Mock parsedPatterns
    // const parsedPatterns = {'a*':}; 
    
    parseWildCards(p).forEach((pattern) => {
      switch (Object.keys(pattern)[0]) {
        case 'char':
          checkingPosition ++;
          resultFlags[checkingPosition] = pattern['char'] === s[checkingPosition];
          break;
      }
    });
    

    console.log("Result flags: ", resultFlags);
    return check(resultFlags);
    // mockResultFlags = ['a', false];
    // console.log("Result flags: ", mockResultFlags);
    // return check(mockResultFlags);
};

const parseWildCards = (p) => {

    var parsePatterns = (p) => {
        // ab.c*d.*
        // a.b*.*c
        const parsers = p.split('');
        var patternArray = [];
        for(i=0; i<parsers.split; i++) {
            if (parser[i] != '.' && parser[i] != '*') {
                
            }
        }
        console.log('Parsed pattern array: ', patternArray);
    }

  // const mockReturn = [
  //   {char: 'a'},
  //   {anyChar: '.'},
  //   {zeroOrMoreChars: 'b*'},
  //   {any: '.*'},
  //   {char: 'c'}
  // ]

    const mockReturn = [
    {char: 'a'},
    {char: 'a'}
  ]
  return mockReturn;
}

const check = (resultFlags) => {
    var pass = true;
    resultFlags.forEach(flag => {
      if (flag === false) {
        pass = false;
        return;
      }
    })
    return pass;
}

const fullTest = (cases) => {
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
  // dot: {s: 'aa', p: 'a.', a: true},
  // asterisk: {s: 'me', p: 'me*', a: true}
};

// run test
fullTest(cases);

// console.log('string'[1])
