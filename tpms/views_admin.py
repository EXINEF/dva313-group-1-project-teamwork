from django.shortcuts import get_object_or_404, redirect, render
from django.contrib.auth.models import User
from .models import CompanyAdministrator, FleetManager
from .forms import CreateUserForm, EditFleetManagerForm
from .decorators import company_administrator_only
from django.contrib.auth.decorators import login_required
from django.contrib import messages

@login_required(login_url='index')
@company_administrator_only
def adminPage(request):
    administrator = CompanyAdministrator.objects.get(user=request.user)
    fleet_managers = FleetManager.objects.filter(company=administrator.company)

    context = {'fleet_managers':fleet_managers}
    return render(request, 'admin/admin-page.html', context)
    
@login_required(login_url='index')
@company_administrator_only
def addFleetManager(request):   
    form = CreateUserForm()
    if request.method == 'POST':
        form = CreateUserForm(request.POST)
        if form.is_valid():
            user = form.save()      
            f = FleetManager.objects.get(user=user)
            a = CompanyAdministrator.objects.get(user=request.user)
            f.company = a.company
            f.save()
            messages.success(request, 'Fleet Manager Account was created for ' + f.user.username)
            return redirect('admin-page')

    context = {'form':form}
    return render(request, 'admin/add-fleet-manager.html', context)

@login_required(login_url='index')
@company_administrator_only
def editFleetManager(request,pk):
    administrator = CompanyAdministrator.objects.get(user=request.user)
    user = get_object_or_404(User, username=pk)
    fleet_manager = get_object_or_404(FleetManager, user=user, company=administrator.company)
    form = EditFleetManagerForm(instance = user)
    
    if request.method == 'POST':
        form = EditFleetManagerForm(request.POST, instance = user)

        if form.is_valid():
            form.save()
            messages.success(request,'The Fleet Manager %s is been update successfuly' % fleet_manager)
            return redirect('admin-page')

        else:
            messages.error(request,'Error while updating Fleet Manager: '+str(form.errors))

    context = {'fleet_manager':fleet_manager, 'form':form}
    return render(request, 'admin/edit-fleet-manager.html', context)

@login_required(login_url='index')
@company_administrator_only
def deleteFleetManager(request, pk):
    administrator = CompanyAdministrator.objects.get(user=request.user)
    user = get_object_or_404(User, username=pk)
    fleet_manager = get_object_or_404(FleetManager, user=user, company=administrator.company)
    
    if request.method == 'POST':
        messages.success(request,'The Fleet Manager %s was deleted successfuly' % fleet_manager)
        User.delete(user)
        return redirect('admin-page')

    context = {'fleet_manager':fleet_manager}
    return render(request, 'admin/delete-fleet-manager.html', context)
