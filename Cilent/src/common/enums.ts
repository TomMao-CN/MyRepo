export namespace SharedEnums {
    /**
     * 管理员类型
     */
    export enum AdminType {
        SuperAdmin = 1,
        SecondaryAdmin = 2,
    }
    /**
     * 角色状态
     */
    export enum RoleStatus {
        //正常
        Normal = 1,
        //审核中
        Verify = 2
    }
    /**
     * 数据状态
     */
    export enum SharedStatus {
        //正常
        Normal = 1,
        //审核中
        Verify = 2
    }
}