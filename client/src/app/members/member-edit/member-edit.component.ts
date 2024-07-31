import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule, FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') form?:NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event: any){
    if(this.form?.dirty){
      $event.returnValue=true;
    }
  }
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);
  private toastr = inject(ToastrService);
  member?: Member;

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const user = this.accountService.currentUser();
    if(!user) return;
    if(this.memberService.members().length === 0){
      this.memberService.getMember(user.username).subscribe({
        next: response => this.member = response
      });
    }
    else {
      this.member = this.memberService.members().find(m => m.userName === user.username);
    }
  }

  updateMember(){
    this.memberService.updateMember(this.form?.value).subscribe({
      next: _ => {
        this.toastr.success('Profile updated successfully');
        this.form?.reset(this.member);  
      }
    })
  }
}
