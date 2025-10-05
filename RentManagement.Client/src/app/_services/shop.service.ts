import { HttpClient } from '@angular/common/http';
import { effect, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { ShopList } from '../_models/shop';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  shops = signal<ShopList[]>([]);
  private loaded = signal(false);

  constructor() {
    effect(() => {
      this.reset();
    });
  }

  reset() {
    this.shops.set([]);
    this.loaded.set(false);
  }

  getShops() {
    return this.http.get<ShopList[]>(`${this.baseUrl}/shop`).subscribe({
      next: (shops) => {
        this.shops.set(shops);
      },
      error: (error) => {
        console.error('Error loading shops:', error);
      },
    });
  }
}
