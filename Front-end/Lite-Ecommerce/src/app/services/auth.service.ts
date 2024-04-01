import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private _HttpClinet:HttpClient) { }

  login(userData:any){
    console.log(userData);
    
    // https://localhost:44341/api/Auth/Login
    return this._HttpClinet.post('https://localhost:5295/api/Auth/Login',userData,{
      headers:{
        'Content-Type': 'application/json',
        
      },
      withCredentials: true
    });
  }

  product(){
    return this._HttpClinet.get('https://localhost:5295/testToken',{
      withCredentials: true
    });
  }
}
