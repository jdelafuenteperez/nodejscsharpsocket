const net = require("net");
const fs = require("fs");

//Creamos el servidor
const server = net.createServer((socket)=>{
    
});

server.on('connection',(socket)=>{
    console.log(`Se ha conectado un nuevo cliente desde ${socket.remoteAddress} : ${socket.remotePort}.`);
    server.getConnections((error,clientsCount)=>{
        if(error)
        {
            console.log(`Se produjo un error al intentar obtener el numero de usuarios conectados`);
        }

        console.log(`Clientes conectados : ${clientsCount}`);
    });
   
    socket.setEncoding('ASCII');
    let receivedData =[];

    socket.on('data',(buff)=>{
        receivedData.push(buff);
    }).on('end',()=>{
        //Los datos están disponibles 
        console.log(receivedData);
        if(receivedData["Avatar"])
        {
            fs.writeFile(`${receivedData["Name"]+" " +receivedData["Surname"]}.jpg`, Buffer.from(receivedData["Avatar"],"base64"),(err)=>{
                if(err)throw err;
                console.log("Imagen correctamente guardada.");
            });
        }

      
        
    });

    socket.on('close', ()=>{
        console.log(`Se desconectó el cliente desde : ${socket.remoteAddress}:${socket.remotePort}`);
        server.getConnections((error,clientsCount)=>{
            if(error)
            {
                console.log(`Se produjo un error al intentar obtener el numero de usuarios conectados`);
            }
    
            console.log(`Clientes conectados : ${clientsCount}`);
        });
    });
    
    socket.once('error', (error)=>{
        console.log("Se produjo un error en la conexión con el cliente : " + error);
    });

});

//Iniciamos el servidor
server.listen(8000, ()=>{
    console.log(`Servidor esperando conexiones en : ${server.address().address}:${server.address().port}`);
});