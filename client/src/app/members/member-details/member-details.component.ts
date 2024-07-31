import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { GalleryItem, GalleryModule, ImageItem } from 'ng-gallery';

@Component({
  selector: 'app-member-details',
  standalone: true,
  imports: [TabsModule, GalleryModule],
  templateUrl: './member-details.component.html',
  styleUrl: './member-details.component.css'
})
export class MemberDetailsComponent implements OnInit {
  private currentRoute = inject(ActivatedRoute);
  private membersService = inject(MembersService);
  member?: Member;
  images: GalleryItem[] = [];


  ngOnInit(): void {
    this.loadMemberDetails();
  }

  loadMemberDetails() {
    const username = this.currentRoute.snapshot.paramMap.get('username');
    if(username){
      if(this.membersService.members().length === 0){
        this.membersService.getMember(username).subscribe({
          next: (response) => {
            this.member = response;
            response.photos.map(p => {
              this.images.push(new ImageItem({src: p.url, thumb: p.url}));
            });
          }
        });
      }
      else {
        this.member = this.membersService.members().find(m => m.userName === username);
        this.member?.photos.map(p => this.images.push(new ImageItem({src: p.url, thumb: p.url})));
      }
    }
  }
}
