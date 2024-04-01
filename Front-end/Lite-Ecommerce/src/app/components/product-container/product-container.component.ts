import { Component, OnInit } from '@angular/core';
import { ProductCardComponent } from '../product-card/product-card.component';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-container',
  standalone: true,
  imports: [ProductCardComponent],
  templateUrl: './product-container.component.html',
  styleUrl: './product-container.component.css'
})
export class ProductContainerComponent implements OnInit{
  /**
   *
   */
  products: any;
  constructor(private _ProductService:ProductService) {
  }
  ngOnInit(): void {
    this._ProductService.getProducts().subscribe({
      next: (data) => {
        console.log(data);
        
        this.products = data;
      },
      error: (error) => {
        console.log(error);
      }
    })
  }

  

}
