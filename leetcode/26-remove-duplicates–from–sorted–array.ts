// https://leetcode.com/problems/remove-duplicates-from-sorted-array/

/**
 * @param {number[]} nums
 * @return {number}
 */


// 3rd Try✅️ (72 ms): Reduce calculation by removing `duplicates[i - 1]`.
const removeDuplicates = (duplicates: number[]) => {
    if (duplicates.length <= 1) return duplicates.length;
    let uniqueCount: number = 0;
    for (let i = 1; i < duplicates.length; i++) {
      if (duplicates[i] !== duplicates[uniqueCount]) {
        uniqueCount++;
        duplicates[uniqueCount] = duplicates[i];
      }
    }
    return uniqueCount + 1;
  }
// 2nd try✅️ (64 ms, 36.7 MB): Comparing array[i-1] with array[i]
// Removed extra console.log() to save time.
const removeDuplicates = (duplicates: number[]) => {
  let uniqueCount: number = 1;
  for (let i = 1; i < duplicates.length; i++) {
    if (duplicates[i] !== duplicates[i - 1]) {
      duplicates[uniqueCount] = duplicates[i];
      uniqueCount++;
    }
  }
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