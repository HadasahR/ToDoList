import axios from 'axios';

const apiUrl = "http://localhost:5268/"
axios.defaults.baseURL="${apiUrl}Items/";
axios.interceptors.response.use(
  (response)=>response,
  (error)=>{
    console.log("error:",error);
  }
);
axios.interceptors.request.use(function (config){
console.log("good");
return config;},function(error){
  console.log("error");
  return Promise.reject(error);
}
);


export default {
  
  //שליפת כל המשימות 
  getTasks: async () => {
    debugger
    const result= await axios.get(`${apiUrl}`) 
     return result.data;
  },
//הוספת משימה
  addTask: async(name)=>{
    debugger
    await axios.post(`${apiUrl}items/${name}`)
    //TODO
  },
//עדכון משימה
  setCompleted: async(id, isComplete)=>{
    debugger
    await axios.put(`${apiUrl}Items/${id}/${isComplete}`)
    return {};
  },
//מחיקת משימה
  deleteTask:async(id)=>{
    debugger
   const result=await axios.delete(`${apiUrl}Items/${id}`)
   return {};
  }
};
