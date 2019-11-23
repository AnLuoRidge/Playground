// async route
app.get('/test', async (req, res) => {
    const msg = await timeout(()=>{console.log('2000 years passed')});
    res.send(msg);
  });

it('', async () => {
const res = await request(app)
    .get('/test');
expect(res.text).toEqual('1s passed');
expect(res.status).toEqual(200);
});

// async function
const timeout = (callback) => new Promise((resolve, reject) => {
    setTimeout(() => {
      console.log('1000 years passed');
      callback();
      resolve('1s passed');
    }, 3000);
});
  
it('', async () => {
const res = await timeout(() => {
    console.log('2000 years passed')
});
expect(res).toEqual('1s passed');
});
