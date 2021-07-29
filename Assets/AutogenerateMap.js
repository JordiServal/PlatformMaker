const fs = require('fs');
const jsonObj = {
    name: "Test2",
    owner: "Pepe2",
    columns: []
}

for(let i = 0; i<1000; i++) {
    const temp = [];
    for(let j = 0; j < 100; j++) {
        temp.push(Math.floor(Math.random() * (1 - 0 + 1) + 0))
    }
    jsonObj.columns.push(temp);
} 


fs.writeFile('map.json', JSON.stringify(jsonObj), () => console.log)
