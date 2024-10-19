import { saveAs } from 'file-saver';
import { Component, OnInit } from '@angular/core';
import { BusinessCardService } from '../../services/business-card.service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-business-card-list',
  templateUrl: './business-card-list.component.html',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
})
export class BusinessCardListComponent implements OnInit {
  businessCards: any[] = [];
  filterForm!: FormGroup;

  isLoading: boolean = false;

  constructor(
    private fb: FormBuilder,
    private businessCardService: BusinessCardService
  ) {}

  ngOnInit(): void {
    this.loadBusinessCards();
    this.filterForm = this.fb.group({
      name: [''],
      dob: [''],
      phone: [''],
      email: [''],
      gender: [''],
    });
  }
  onFilterSubmit(): void {
    if (this.filterForm.valid) {
      const filterValues = this.filterForm.value;

      this.businessCardService.filterBusinessCards(filterValues).subscribe(
        (data) => {
          this.businessCards = data.data;
        },
        (error) => {
          console.error('Error filtering business cards', error);
        }
      );
    }
  }
  clearFilters(): void {
    this.filterForm.reset({
      name: '',
      dob: '',
      phone: '',
      email: '',
      gender: '',
    });
    this.loadBusinessCards();
  }
  loadBusinessCards(): void {
    this.isLoading = true;
    this.businessCardService.getBusinessCards().subscribe({
      next: (response) => {
        if (response.success) {
          this.businessCards = response.data;
        } else {
          console.error(response.message);
        }
      },
      error: (error) => {
        console.error('Error fetching business cards:', error);
      },
      complete: () => {
        this.isLoading = false;
      },
    });
  }
  deleteBusinessCard(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.businessCardService.deleteBusinessCard(id).subscribe({
          next: (response) => {
            if (response.success) {
              this.businessCards = this.businessCards.filter(
                (card) => card.id !== id
              );
              Swal.fire(
                'Deleted!',
                'Business card has been deleted successfully.',
                'success'
              );
            } else {
              console.error(response.message);
            }
          },
          error: (error) => {
            console.error('Error deleting business card:', error);
            Swal.fire(
              'Error!',
              'An error occurred while deleting the business card.',
              'error'
            );
          },
        });
      }
    });
  }

  exportBusinessCardsAsCsv(): void {
    this.exportBusinessCards('csv');
  }

  exportBusinessCardsAsXml(): void {
    this.exportBusinessCards('xml');
  }

  private exportBusinessCards(format: string): void {
    this.businessCardService.exportBusinessCards(format).subscribe({
      next: (response) => {
        const contentDisposition = response.headers.get('content-disposition');
        const filename = contentDisposition
          ? contentDisposition.split('filename=')[1]
          : `business-cards.${format}`;
        saveAs(response.body, filename);
      },
      error: (error) => {
        console.error(`Error exporting business cards as ${format}:`, error);
      },
    });
  }
}
