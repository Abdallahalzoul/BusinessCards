

























































































































import { Component } from '@angular/core';
import { BusinessCardService } from '../../services/business-card.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BusinessCardPreviewComponent } from '../business-card-preview/business-card-preview.component';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-business-card-add',
  standalone: true,
  imports: [CommonModule, FormsModule, BusinessCardPreviewComponent, RouterLink],
  templateUrl: './business-card-add.component.html',
})
export class BusinessCardAddComponent {
  businessCard = {
    name: '',
    gender: '',
    dateOfBirth: '',
    phone: '',
    email: '',
    address: '',
    image: null as File | null,
  };

  previewUrl: string | null = null;
  bulkFile: File | null = null;

  constructor(private businessCardService: BusinessCardService) {}

  onFileDropped(event: any) {
    const file = event[0];
    this.handleFileInput(file);
  }

  handleFileInput(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        const result = reader.result;
        if (typeof result === 'string') {
          this.previewUrl = result;
        }
      };
      reader.readAsDataURL(file);
      this.businessCard.image = file;
    }
  }



  addBusinessCard() {
    const formData = new FormData();
    formData.append('Name', this.businessCard.name);
    formData.append('Gender', this.businessCard.gender);
    formData.append('DateOfBirth', this.businessCard.dateOfBirth);
    formData.append('Phone', this.businessCard.phone);
    formData.append('Email', this.businessCard.email);
    formData.append('Address', this.businessCard.address);
    if (this.businessCard.image) {
      formData.append('PhotoFile', this.businessCard.image);
    }

    this.businessCardService.addBusinessCard(formData).subscribe({
      next: (response) => {
        Swal.fire({
          toast: true,
          icon: 'success',
          title: 'Business card added successfully!',
          position: 'top-end',
          showConfirmButton: false,
          timer: 3000,
          timerProgressBar: true,
        });
      },
      error: (error) => {
        Swal.fire({
          toast: true,
          icon: 'error',
          title: 'Error adding business card!',
          position: 'top-end',
          showConfirmButton: false,
          timer: 3000,
          timerProgressBar: true,
        });
        console.error('Error adding business card:', error);
      }
    });
  }
}
