import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http=inject(HttpClient);
  baseUel='https://localhost:5001/api/'

  login(model: any)
  {
    return this.http.post(this.baseUel+'account/login',model);
  }
}
