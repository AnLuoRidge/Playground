/**
 * @param {number[]} nums
 * @return {number}
 */

 // 2nd try✅️ (64 ms, 36.7 MB): Comparing array[i-1] with array[i]
 // Removed extra console.log() to save time.
 const removeDuplicates = (input) => {
    let uniqueCount = 1;
    for (let i = 1; i < input.length; i++) {
      if (input[i] !== input[i - 1]) {
        input[uniqueCount] = input[i];
        uniqueCount++;
      }
    }
    console.log(`${uniqueCount}: [${input}]`);
    return uniqueCount;
  }

 // 1st try✅️ (404 ms, 42.9 MB): Using ES7 `Array.includes()`
 // Without the notice of SORTED array

 /*
 const removeDuplicates = (input: number[]) => {
     let uniqueCount: number = 1; 
     for (let i = 1; i < input.length; i ++) {
       if (input.slice(0, i).includes(input[i])) {
      } else {
        input[uniqueCount] = input[i];
        uniqueCount ++;
    }
  }
    console.log(`${uniqueCount}: [${input}]`);
    return uniqueCount;
  }
  */

  // const input1 = [1, 1, 2];
  // const input2 = [1, 1, 1, 2, 2, 2, 2, 3];
  // removeDuplicates(input1);
  // removeDuplicates(input2);