import { HttpClient } from '@angular/common/http';
import { effect, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { ShopList, ShopUpdate } from '../_models/shop';
import { Account } from './account';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;
  private account = inject(Account);

  shops = signal<ShopList[]>([]);

  constructor() {
    effect(() => {
      const user = this.account.currentUser();
      this.shops.set([]);
      if (user) {
        this.getShops();
      }
    });
  }

  getShop(id: number) {
    return this.http.get<ShopList>(`${this.baseUrl}/shop/${id}`);
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

  getShopForUpdate(id: number) {
    return this.http.get<ShopUpdate>(`${this.baseUrl}/shop/${id}`);
  }

  updateShop(id: number, shop: ShopUpdate) {
    return this.http.put<ShopUpdate>(`${this.baseUrl}/shop/${id}`, shop).pipe(
      tap(() => {
        this.getShop(id);
        this.getShops();
      })
    );
  }

  deleteShop(id: number) {
    return this.http.delete(`${this.baseUrl}/shop/${id}`).pipe(
      tap(() => {
        this.getShops();
      })
    );
  }
}
