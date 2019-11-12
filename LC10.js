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
const v = 1; // verbose

var isMatch = (s, p) => {
    // create a empty or with sth length-defined array
    // var foo = new Array(45); // create an empty array with length 45
    
    

    var resultFlags = new Array(s.split('').length);
    for (i = 0; i < resultFlags.length; i++)
    // resultFlags.forEach((flag, index) => {
    //     this[index] = false;
    // }, resultFlags);
    if (v === 1) { console.log('Initial result flags: ', resultFlags)};
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
          if (v === 1) { console.log(`Checking position[${checkingPosition}]: ${s[checkingPosition]} | ${pattern['char']}\nResult: ${resultFlags[checkingPosition]}`)}
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
        // ab.c*d.*
        // a.b*.*c

        const chars = p.split('');
        var patterns = [];
        for(i = 0; i < chars.length; i++) {
          const hasNext = i+1 < chars.length;
          const next = hasNext ? chars[i+1] : '';
          // debug following
          // String.prototype.isChar = (char) => {return char != '.' && char != '*'};
          const isChar = (char) => {return char !== '.' && char !== '*'};
          const isDot = (char) => { return char === '.'};
          // add func to String
          // Start with char
            if (isChar(chars[i])) {
                if (hasNext) {
                  if (isChar(next) || isDot(next)) { // char + char || char.
                    patterns.push({char: chars[i]});
                    if (v === 1) { console.log(`Round ${i}: 'char + char || char. : ${chars[i]}`)};
                  } else if (next === '*') { // char + *
                    patterns.push({zeroOrMoreChars: chars[i]});
                    if (v === 1) { console.log(`Round ${i}: 'char + *: ${chars[i]}`)};
                    i++;
                  }
                } else { // char EOF
                    patterns.push({char: chars[i]});
                    if (v === 1) { console.log(`Round ${i}: 'char EOF: ${chars[i]}`)};
                }
            }
        }
        console.log('Parsed pattern array: ', patterns);
        return patterns;

  // const mockReturn = [
  //   {char: 'a'},
  //   {anyChar: '.'},
  //   {zeroOrMoreChars: 'b*'},
  //   {any: '.*'},
  //   {char: 'c'}
  // ]

  //   const mockReturn = [
  //   {char: 'a'},
  //   {char: 'a'}
  // ]
  // return mockReturn;
}

const check = (resultFlags) => {
    var pass = true;
    resultFlags.forEach(flag => {
      if (flag !== true) {
        pass = false;
        return;
      }
    })
    return pass;
}

// const testCheckResultFlags = (resultFlagsArray) => {
//   for 
// }


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
  fullMatch: {s: 'ab', p: 'ab', a: true}, 
  fullMatch2: {s: 'ab', p: 'ac', a: false},
  fullMatch3: {s: 'radio', p: 'radio', a: true},
  fullMatch4: {s: 'Radio', p: 'radio', a: false},
  asterisk: {s: 'me', p: 'me*', a: true},
    // dot: {s: 'aa', p: 'a.', a: true},
};

// run test
fullTest(cases);

// console.log('string'[1])