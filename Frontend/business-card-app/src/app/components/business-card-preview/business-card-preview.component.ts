import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-business-card-preview',
  standalone: true,
  imports: [NgIf],
  templateUrl: './business-card-preview.component.html',
  styleUrls: ['./business-card-preview.component.css']
})
export class BusinessCardPreviewComponent {
  @Input() name!: string;
  @Input() gender!: string;
  @Input() dateOfBirth!: string;
  @Input() phone!: string;
  @Input() email!: string;
  @Input() address!: string;
  @Input() imageUrl!: string | null;

  get displayName(): string {
    return this.name ? this.name : 'No Name';
  }

  get displayGender(): string {
    return this.gender ? this.gender : 'Not Selected';
  }

  get displayAge(): string {
    if (!this.dateOfBirth) {
      return 'No Date of Birth';
    }

    const birthDate = new Date(this.dateOfBirth);
    const today = new Date();
    let age = today.getFullYear() - birthDate.getFullYear();
    const monthDifference = today.getMonth() - birthDate.getMonth();

    if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }

    return `${age} years old`;
  }

  get displayPhone(): string {
    return this.phone ? this.phone : 'No Phone';
  }

  get displayEmail(): string {
    return this.email ? this.email : 'No Email';
  }

  get displayAddress(): string {
    return this.address ? this.address : 'No Address';
  }

  get displayImageUrl(): string {
    return this.imageUrl ? this.imageUrl : 'https://via.placeholder.com/350x200';
  }
}
