import { Component } from '@angular/core';
import { ProductContainerComponent } from '../../components/product-container/product-container.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ProductContainerComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
