import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AccountService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';
  currentUser = signal<User | null>(null);

  login(model: any){
    return this.http.post<User>(this.baseUrl  + 'account/login', model).pipe(
      map(u => {
        if(u){
          localStorage.setItem('user', JSON.stringify(u));
          this.currentUser.set(u);
        }

        return u;
      })
    );
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl  + 'account/register', model).pipe(
      map(u => {
        if(u){
          localStorage.setItem('user', JSON.stringify(u));
          this.currentUser.set(u);
        }

        return u;
      })
    );
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
