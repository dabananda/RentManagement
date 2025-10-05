import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { ShopList } from '../_models/shop';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  shops = signal<ShopList[]>([]);

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
