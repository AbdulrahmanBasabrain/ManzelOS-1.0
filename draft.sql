USE [ManzelOSDB]
GO
/****** Object:  StoredProcedure [dbo].[SP_ProcessAndMakePayment]    Script Date: 10/12/2025 1:49:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SP_ProcessAndMakePayment]

--- targeted bill info ---
	@billId int,
--- payment info ---
	@paidAmount smallmoney,
	@paidByTenant int,
	@received_by_owner_id int,
	@paymentDate datetime2,
	@paymentMethodId smallint,
	@paymentStatusId smallint,
	@createdAt datetime2,
	@paymentId int output

as 
begin 

	declare @billFees smallmoney;
	declare @billPaymentStatus smallint;
	declare @previousPaymentsSum smallmoney;
	declare @totalBill smallmoney;

	select @billPaymentStatus = bill_payment_status_id from generated_bills where generated_bill_id = @billId;
	select @previousPaymentsSum = sum(payment_amonut) from payments where generated_bill_id = @billId;
	if @previousPaymentsSum = null 
		begin

			set @previousPaymentsSum = 0;		 
		 
		end	

	select @billFees = generated_bill_fees from generated_bills where generated_bill_id = @billId;
	set @totalBill = @paidAmount + @previousPaymentsSum;

	if (@billPaymentStatus != 1)
		begin
			if @totalBill = @billFees
				begin
					begin transaction
						begin try
							INSERT INTO [dbo].[payments]
							([payment_amonut]
							,[paid_by_tenant_id]
							,[generated_bill_id]
							,[received_by_rental_unit_owner_id]
							,[payment_date]
							,[payment_method_id]
							,[payment_status_id]
							,[created_at])
							VALUES
							(
								@paidAmount,
								@paidByTenant,
								@billId,
								@received_by_owner_id,
								@paymentDate,
								@paymentMethodId,
								@paymentStatusId,
								@createdAt
							);
							set @paymentId = SCOPE_IDENTITY();
							update generated_bills
								set bill_payment_status_id = 1
								where generated_bill_id = @billId;
								
							commit
						end try
						begin catch
							rollback
						end catch
				end
            
			if @totalBill > 0 and @totalBill < @billFees
				begin
					begin transaction
						begin try
							INSERT INTO [dbo].[payments]
							([payment_amonut]
							,[paid_by_tenant_id]
							,[generated_bill_id]
							,[received_by_rental_unit_owner_id]
							,[payment_date]
							,[payment_method_id]
							,[payment_status_id]
							,[created_at])
							VALUES
							(
								@paidAmount,
								@paidByTenant,
								@billId,
								@received_by_owner_id,
								@paymentDate,
								@paymentMethodId,
								@paymentStatusId,
								@createdAt
							)
							set @paymentId = SCOPE_IDENTITY();
							update generated_bills
								set bill_payment_status_id = 3
								where generated_bill_id = @billId;
								
							commit
						end try
						begin catch
							rollback
						end catch
				end
		end
end
