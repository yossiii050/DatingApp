import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http=inject(HttpClient);
  baseUrl=environment.apiUrl;
  members=signal<Member[]>([]);

  getMembers(){
    return this.http.get<Member[]>(this.baseUrl+'user').subscribe({
      next: members=>this.members.set(members)
    });
  }

  getMember(username: string){
    return this.http.get<Member>(this.baseUrl+'user/'+username);
  }

  updateMember(member:Member){
    return this.http.put(this.baseUrl+'user',member);
  }
}
