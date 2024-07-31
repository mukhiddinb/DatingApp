import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private httpClient = inject(HttpClient);
  baseUrl = environment.apiUrl;
  members = signal<Member[]>([]);

  getMembers() {
    if(this.members().length == 0){
      this.httpClient.get<Member[]>(this.baseUrl + 'users').subscribe({
        next: response => this.members.set(response)
      });
    }

    return this.members();
  }

  getMember(username: string) {
    return this.httpClient.get<Member>(this.baseUrl + 'users/' + username);
  }

  updateMember(member: Member) {
    return this.httpClient.put(this.baseUrl + 'users', member).pipe(
      tap(() => {
        this.members.update(members => members.map(m => m.userName === member.userName ? member : m));
      })
    );
  }
}
