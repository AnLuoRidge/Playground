/**
 * @param {number[]} nums
 * @return {number}
 */

 // 1st try✅️ (404 ms, 42.9 MB): Using ES7 `Array.includes()`
 // Without the notice of SORTED array
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
  
  // let input1 = [1, 1, 2];
  // let input2 = [1, 1, 1, 2, 2, 2, 2, 3];
  // removeDuplicates(input1);
  // removeDuplicates(input2);