// 1st Try: 64 ms

var rotate = function(nums, k) {
    k = k % nums.length;
    const head = nums.slice(nums.length - k, nums.length);
    for (let i = nums.length - 1; i >= 0; i --) {
        nums[i] = nums[i - k];
    }
    head.forEach((ele, index)=>{
      nums[index] = ele;
    })
};
