import { Component, inject, OnInit, signal } from '@angular/core';
import { ShopService } from '../../_services/shop.service';
import { ActivatedRoute } from '@angular/router';
import { ShopList } from '../../_models/shop';

@Component({
  selector: 'app-shop',
  imports: [],
  templateUrl: './shop.html',
  styleUrl: './shop.css',
})
export class Shop implements OnInit {
  private shopService = inject(ShopService);
  private route = inject(ActivatedRoute);
  shop = signal<ShopList | null>(null);

  ngOnInit(): void {
    this.loadShop();
  }

  loadShop() {
    const id: number = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) return;
    this.shopService.getShop(id).subscribe({
      next: (shop: any) => this.shop.set(shop),
      error: (err) => console.error('Error loading shop:', err),
    });
  }
}
