import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private _HttpClinet:HttpClient) { }

  getProducts(){
    return this._HttpClinet.get('http://localhost:5295/api/Product',{
      withCredentials: true
    });
  }

}
