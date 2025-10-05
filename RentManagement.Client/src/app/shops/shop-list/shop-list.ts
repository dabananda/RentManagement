import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../_services/shop.service';

@Component({
  selector: 'app-shop-list',
  imports: [],
  templateUrl: './shop-list.html',
  styleUrl: './shop-list.css',
})
export class ShopList implements OnInit {
  shopService = inject(ShopService);

  ngOnInit(): void {
    if (this.shopService.shops().length === 0) {
      this.shopService.getShops();
    }
  }
}
