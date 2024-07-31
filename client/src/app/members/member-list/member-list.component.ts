import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from '../member-card/member-card.component';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  membersService = inject(MembersService);
  //members: Member[] = [];

  ngOnInit(): void {
    //this.loadMembers();
    this.membersService.getMembers();
  }

  //loadMembers(): void {
    // this.membersService.getMembers().subscribe({
    //   next: response => this.members = response
    // });
    //this.members = this.membersService.getMembers();
  //}
}
